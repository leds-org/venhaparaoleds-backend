import request from 'supertest';
import { createApp } from '../../index';
import { prisma } from '../../infrastructure/database/connection';

describe('Integration Tests', () => {
  let app: any;

  beforeAll(async () => {
    app = createApp();
    
    await prisma.candidato.deleteMany();
    await prisma.concurso.deleteMany();

    await prisma.candidato.create({
      data: {
        nome: 'João Silva',
        dataDeNascimento: new Date('1990-01-01'),
        cpf: '123.456.789-01',
        profissoes: JSON.stringify(['engenheiro civil', 'arquiteto']),
      },
    });

    await prisma.concurso.create({
      data: {
        orgao: 'SEDU',
        edital: '1/2023',
        codigo: '12345678901',
        vagas: JSON.stringify(['engenheiro civil', 'professor']),
      },
    });
  });

  afterAll(async () => {
    await prisma.candidato.deleteMany();
    await prisma.concurso.deleteMany();
    await prisma.$disconnect();
  });

  describe('GET /api/candidatos/:cpf/concursos', () => {
    it('should return concursos for valid candidato', async () => {
      const response = await request(app)
        .get('/api/candidatos/123.456.789-01/concursos')
        .expect(200);

      expect(response.body.status).toBe('success');
      expect(response.body.data.concursos).toHaveLength(1);
      expect(response.body.data.concursos[0]).toEqual({
        orgao: 'SEDU',
        codigo: '12345678901',
        edital: '1/2023',
      });
    });

    it('should return 404 for non-existent candidato', async () => {
      await request(app)
        .get('/api/candidatos/999.999.999-99/concursos')
        .expect(404);
    });

    it('should return 400 for invalid CPF format', async () => {
      await request(app)
        .get('/api/candidatos/invalid-cpf/concursos')
        .expect(400);
    });
  });

  describe('GET /api/concursos/:codigo/candidatos', () => {
    it('should return candidatos for valid concurso', async () => {
      const response = await request(app)
        .get('/api/concursos/12345678901/candidatos')
        .expect(200);

      expect(response.body.status).toBe('success');
      expect(response.body.data.candidatos).toHaveLength(1);
      expect(response.body.data.candidatos[0]).toEqual({
        nome: 'João Silva',
        dataDeNascimento: '1990-01-01T00:00:00.000Z',
        cpf: '123.456.789-01',
      });
    });

    it('should return 404 for non-existent concurso', async () => {
      await request(app)
        .get('/api/concursos/99999999999/candidatos')
        .expect(404);
    });

    it('should return 400 for invalid codigo format', async () => {
      await request(app)
        .get('/api/concursos/invalid-codigo/candidatos')
        .expect(400);
    });
  });

  describe('GET /api/health', () => {
    it('should return health status', async () => {
      const response = await request(app)
        .get('/api/health')
        .expect(200);

      expect(response.body.status).toBe('success');
      expect(response.body.message).toBe('API is running');
    });
  });
});
