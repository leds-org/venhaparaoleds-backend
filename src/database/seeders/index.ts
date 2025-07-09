import { readFileSync } from 'fs';
import { resolve } from 'path';
import { prisma } from '../../infrastructure/database/connection';
import { ICandidato, IConcurso } from '../../domain/entities';
import { validateDate } from '../../utils/validators';
import { logger } from '../../utils/logger';

function parseProfissoes(profissoesStr: string): string[] {
  const cleanStr = profissoesStr.replace(/[\[\]"]/g, '');
  return cleanStr.split(',').map(p => p.trim()).filter(p => p.length > 0);
}

async function seedCandidatos(): Promise<void> {
  logger.info('Loading candidatos from file...');
  
  const filePath = resolve(__dirname, '../../../candidatos.txt');
  const fileContent = readFileSync(filePath, 'utf-8');
  const lines = fileContent.trim().split('\n');
  
  const dataLines = lines.slice(1);
  
  const candidatos: ICandidato[] = [];
  
  for (const line of dataLines) {
    const [nome, dataStr, cpf, profissoesStr] = line.split(',');
    
    if (!nome || !dataStr || !cpf || !profissoesStr) {
      logger.warn(`Skipping invalid line: ${line}`);
      continue;
    }
    
    try {
      const dataDeNascimento = validateDate(dataStr.trim());
      const profissoes = parseProfissoes(profissoesStr.trim());
      
      candidatos.push({
        nome: nome.trim(),
        dataDeNascimento,
        cpf: cpf.trim(),
        profissoes,
      });
    } catch (error) {
      logger.warn(`Error parsing line: ${line}`, error);
    }
  }
  
  await prisma.candidato.deleteMany();
  
  const batchSize = 100;
  for (let i = 0; i < candidatos.length; i += batchSize) {
    const batch = candidatos.slice(i, i + batchSize);
    await prisma.candidato.createMany({
      data: batch.map(candidato => ({
        nome: candidato.nome,
        dataDeNascimento: candidato.dataDeNascimento,
        cpf: candidato.cpf,
        profissoes: JSON.stringify(candidato.profissoes),
      })),
    });
  }
  
  logger.info(`Loaded ${candidatos.length} candidatos`);
}

async function seedConcursos(): Promise<void> {
  logger.info('Loading concursos from file...');
  
  const filePath = resolve(__dirname, '../../../concursos.txt');
  const fileContent = readFileSync(filePath, 'utf-8');
  const lines = fileContent.trim().split('\n');
  
  const dataLines = lines.slice(1);
  
  const concursos: IConcurso[] = [];
  const codigosVistos = new Set<string>();
  
  for (const line of dataLines) {
    const [orgao, edital, codigo, vagasStr] = line.split(',');
    
    if (!orgao || !edital || !codigo || !vagasStr) {
      logger.warn(`Skipping invalid line: ${line}`);
      continue;
    }
    
    if (codigosVistos.has(codigo.trim())) {
      logger.warn(`Skipping duplicate codigo: ${codigo.trim()}`);
      continue;
    }
    
    try {
      const vagas = parseProfissoes(vagasStr.trim());
      
      concursos.push({
        orgao: orgao.trim(),
        edital: edital.trim(),
        codigo: codigo.trim(),
        vagas,
      });
      
      codigosVistos.add(codigo.trim());
    } catch (error) {
      logger.warn(`Error parsing line: ${line}`, error);
    }
  }
  
  await prisma.concurso.deleteMany();
  
  const batchSize = 100;
  for (let i = 0; i < concursos.length; i += batchSize) {
    const batch = concursos.slice(i, i + batchSize);
    await prisma.concurso.createMany({
      data: batch.map(concurso => ({
        orgao: concurso.orgao,
        edital: concurso.edital,
        codigo: concurso.codigo,
        vagas: JSON.stringify(concurso.vagas),
      })),
    });
  }
  
  logger.info(`Loaded ${concursos.length} concursos`);
}

export async function runSeed(): Promise<void> {
  try {
    logger.info('Starting database seed...');
    
    await seedCandidatos();
    await seedConcursos();
    
    logger.info('Database seed completed successfully');
  } catch (error) {
    logger.error('Error during database seed:', error);
    throw error;
  }
}

if (require.main === module) {
  runSeed()
    .then(() => {
      logger.info('Seed script completed');
      process.exit(0);
    })
    .catch((error) => {
      logger.error('Seed script failed:', error);
      process.exit(1);
    });
}
