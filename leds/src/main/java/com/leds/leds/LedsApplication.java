package com.leds.leds;

import com.leds.leds.models.Candidatos;
import com.leds.leds.models.Concursos;
import com.leds.leds.models.Profissao;
import com.leds.leds.models.Vagas;
import com.leds.leds.repositories.CandidatosRepository;
import com.leds.leds.repositories.ConcursosRepository;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

import java.io.BufferedReader;
import java.io.FileReader;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;

@SpringBootApplication
public class LedsApplication implements CommandLineRunner {

    @Autowired
    private CandidatosRepository candidatosRepository;

    @Autowired
    private ConcursosRepository concursosRepository;

    public static void main(String[] args) {
        SpringApplication.run(LedsApplication.class, args);
    }

    @Override
    public void run(String... args) throws Exception {
        importarCandidatos();
        importarConcursos();
    }

    private void importarCandidatos() {
        String path = "candidatos.txt";

        try (BufferedReader reader = new BufferedReader(new FileReader(Paths.get(path).toFile()))) {
            String linha;

            while ((linha = reader.readLine()) != null) {
                int profissaoIndex = linha.indexOf("[");
                String dadosPessoais = linha.substring(0, profissaoIndex).trim();
                String profissoesBrutas = linha.substring(profissaoIndex).replace("[", "").replace("]", "").trim();

                String[] partes = dadosPessoais.split("\\s+(?=\\d{2}/\\d{2}/\\d{4})");
                String nome = partes[0].trim();
                String restante = partes[1].trim();
                String[] infoRestante = restante.split("\\s+");
                String dataNascimento = infoRestante[0];
                String cpf = infoRestante[1];

                Candidatos candidato = new Candidatos();
                candidato.setNome(nome);
                candidato.setDataNascimento(dataNascimento);
                candidato.setCpf(cpf);

                List<Profissao> profissoes = new ArrayList<>();
                for (String p : profissoesBrutas.split(",")) {
                    Profissao profissao = new Profissao();
                    profissao.setNome(p.trim());
                    profissao.setCandidato(candidato);
                    profissoes.add(profissao);
                }

                candidato.setProfissoes(profissoes);
                candidatosRepository.save(candidato);
            }

            System.out.println("Candidatos importados com sucesso.");
        } catch (Exception e) {
            System.err.println("Erro ao importar candidatos: " + e.getMessage());
        }
    }

    private void importarConcursos() {
        String pathConcursos = "concursos.txt";

        try (BufferedReader reader = new BufferedReader(new FileReader(Paths.get(pathConcursos).toFile()))) {
            String linha;

            while ((linha = reader.readLine()) != null) {
                int indexAbreColchete = linha.indexOf("[");
                String dados = linha.substring(0, indexAbreColchete).trim();
                String vagasStr = linha.substring(indexAbreColchete).replace("[", "").replace("]", "").trim();

                String[] partes = dados.split("\\s+");

                String orgao = partes[0];
                String edital = partes[1];
                String codigo = partes[2];

                Concursos concurso = new Concursos();
                concurso.setOrgao(orgao);
                concurso.setEdital(edital);
                concurso.setCodigoDoConcurso(codigo);

                List<Vagas> listaDeVagas = new ArrayList<>();
                for (String vagaNome : vagasStr.split(",")) {
                    Vagas vaga = new Vagas();
                    vaga.setCargo(vagaNome.trim());
                    vaga.setConcurso(concurso);
                    listaDeVagas.add(vaga);
                }

                concurso.setListaDeVagas(listaDeVagas);
                concursosRepository.save(concurso);
            }

            System.out.println("Concursos importados com sucesso.");
        } catch (Exception e) {
            System.err.println("Erro ao importar concursos: " + e.getMessage());
        }
    }
}
