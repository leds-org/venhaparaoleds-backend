package Pedro.Artur.BackendDesafioLeds.service;

import Pedro.Artur.BackendDesafioLeds.model.Candidato;
import Pedro.Artur.BackendDesafioLeds.model.Concurso;
import Pedro.Artur.BackendDesafioLeds.repository.CandidatoRepository;
import Pedro.Artur.BackendDesafioLeds.repository.ConcursoRepository;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class ConcursoService {
    public final ConcursoRepository concursoRepository;
    public final CandidatoRepository candidatoRepository;

    public ConcursoService(ConcursoRepository concursoRepository, CandidatoRepository candidatoRepository){
        this.concursoRepository = concursoRepository;
        this.candidatoRepository = candidatoRepository;
    }

    public List<Concurso> getAll(){
        return concursoRepository.findAll();
    }

    public Concurso save(Concurso concurso){
        return concursoRepository.save(concurso);
    }

    public List<Concurso> findByCodigo(Long codigo){
        return concursoRepository.findByCodigo(codigo);
    }

    public List<Concurso> BuscarConcursosCompativeisPorCpf(String cpf){
        Candidato candidato = candidatoRepository.findByCpf(cpf);
        List<String> profissoes = candidato.getProfissoes();

        return concursoRepository.BuscarConcursosCompativeisPorCpf(profissoes);
    }

}
