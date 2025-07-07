package Pedro.Artur.BackendDesafioLeds.service;

import Pedro.Artur.BackendDesafioLeds.dtos.CandidatoResponseDTO;
import Pedro.Artur.BackendDesafioLeds.dtos.ConcursoResponseDTO;
import Pedro.Artur.BackendDesafioLeds.exception.NoMatchCandidatoException;
import Pedro.Artur.BackendDesafioLeds.exception.NoMatchConcursoException;
import Pedro.Artur.BackendDesafioLeds.model.Candidato;
import Pedro.Artur.BackendDesafioLeds.model.Concurso;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Set;
import java.util.stream.Collectors;

@Service
public class MatchingService {
    private final ConcursoService concursoService;
    private final CandidatoService candidatoService;

    public MatchingService(CandidatoService candidatoService, ConcursoService concursoService){
        this.candidatoService = candidatoService;
        this.concursoService = concursoService;
    }

    public List<ConcursoResponseDTO> buscarConcursosCompativeisPorCpf(String cpf) {
        Candidato candidato = candidatoService.buscarPorCpf(cpf);
        List<String> profissoes = candidato.getProfissoes();

        List<ConcursoResponseDTO> concursos = concursoService.buscarPorProfissoes(profissoes);
        if(concursos.isEmpty()){
            throw new NoMatchConcursoException(cpf);
        }
        return concursos;
    }

    public List<CandidatoResponseDTO> buscarCandidatosCompativeisPorCodigoConcurso(Long codigo) {
        List<Concurso> concursos = concursoService.buscarPorCodigo(codigo);
        Set<String> profissoes = concursos.stream()
                .flatMap(concurso -> concurso.getProfissoes().stream())
                .collect(Collectors.toSet());

        List<CandidatoResponseDTO> candidatos = candidatoService.buscarPorProfissoes(profissoes);
        if(candidatos.isEmpty()){
            throw new NoMatchCandidatoException();
        }
        return candidatos;
    }


}
