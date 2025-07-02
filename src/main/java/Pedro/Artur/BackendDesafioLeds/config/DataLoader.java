package Pedro.Artur.BackendDesafioLeds.config;

import Pedro.Artur.BackendDesafioLeds.model.Candidato;
import Pedro.Artur.BackendDesafioLeds.model.Concurso;
import Pedro.Artur.BackendDesafioLeds.service.CandidatoService;
import Pedro.Artur.BackendDesafioLeds.service.ConcursoService;
import org.springframework.boot.CommandLineRunner;
import org.springframework.stereotype.Component;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.Arrays;
import java.util.List;

@Component
public class DataLoader implements CommandLineRunner {
    private final CandidatoService candidatoService;
    private final ConcursoService concursoService;

    public DataLoader(CandidatoService candidatoSerive, ConcursoService concursoService){
        this.candidatoService = candidatoSerive;
        this.concursoService = concursoService;
    }

    public List<String> parseToList(String raw){
        String RawList = raw.substring(1, raw.length()-1);
        return Arrays.stream(RawList.split(",")).map(String::trim).toList();
    }

    //transforma uma string em um objeto Candidato
    public Candidato parseCandidato(String raw){
        String[] dados = raw.split(",", 4);
        String nome = dados[0];
        String cpf = dados[1];
        String dataNasc = dados[2];
        List<String> profissoes = parseToList(dados[3]);

        return new Candidato(null,nome,cpf,dataNasc,profissoes);
    }

    //lê as linhas do arquivo candidatos.txt e os salva no banco de dados
    public void carregaCandidato() throws IOException {
        Path path = Paths.get("src/main/resources/candidatos.txt");
        List<String> line = Files.readAllLines(path);

        for(String raw: line){
            Candidato candidato = parseCandidato(raw);
            candidatoService.save(candidato);
        }
    }
    //transforma uma string em um objeto Concurso
    public Concurso parseConcurso(String raw){
        String[] dados = raw.split(",", 4);
        String orgao = dados[0];
        String edital = dados[1];
        Long codigo = Long.parseLong(dados[2]);
        List<String> profissoes = parseToList(dados[3]);

        return new Concurso(null,orgao,edital,codigo,profissoes);
    }

    //carrega os dados do arquivo concurso.txt no banco de dados
    public void carregaConcurso() throws IOException {
        Path path = Paths.get("src/main/resources/concursos.txt");
        List<String> line = Files.readAllLines(path);

        for(String raw: line){
            Concurso concurso = parseConcurso(raw);
            concursoService.save(concurso);
        }
    }

    @Override
    public void run(String... args) throws IOException {
        carregaCandidato();
        carregaConcurso();

    }
}
