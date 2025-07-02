package Pedro.Artur.BackendDesafioLeds.service;

import Pedro.Artur.BackendDesafioLeds.model.Candidato;
import Pedro.Artur.BackendDesafioLeds.model.Concurso;
import Pedro.Artur.BackendDesafioLeds.repository.CandidatoRepository;
import Pedro.Artur.BackendDesafioLeds.repository.ConcursoRepository;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Set;
import java.util.stream.Collectors;

@Service
public class CandidatoService {
    public final CandidatoRepository candidatoRepository;
    public final ConcursoRepository concursoRepository;

    public CandidatoService(CandidatoRepository candidatoRepository, ConcursoRepository concursoRepository) {
        this.candidatoRepository = candidatoRepository;
        this.concursoRepository = concursoRepository;
    }

    public List<Candidato> getAll(){
        return candidatoRepository.findAll();
    }

    public Candidato save(Candidato candidato){
        return candidatoRepository.save(candidato);
    }

    public Candidato findByCpf(String cpf){
        return candidatoRepository.findByCpf(cpf);
    }

    public List<Candidato> findCandidatosCompativeis(Long codigo){
        List<Concurso> concursos = concursoRepository.findByCodigo(codigo);
        Set<String> profissoes = concursos.stream()
                .flatMap(concurso -> concurso.getProfissoes().stream())
                .collect(Collectors.toSet());

        return candidatoRepository.findCandidatosCompativeis(profissoes);
    }
}
