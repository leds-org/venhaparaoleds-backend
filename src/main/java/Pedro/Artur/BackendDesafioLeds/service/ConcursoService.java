package Pedro.Artur.BackendDesafioLeds.service;

import Pedro.Artur.BackendDesafioLeds.model.Concurso;
import Pedro.Artur.BackendDesafioLeds.repository.ConcursoRepository;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class ConcursoService {
    public final ConcursoRepository concursoRepository;

    public ConcursoService(ConcursoRepository concursoRepository){
        this.concursoRepository = concursoRepository;
    }

    public List<Concurso> getAll(){
        return concursoRepository.findAll();
    }

    public Concurso save(Concurso concurso){
        return concursoRepository.save(concurso);
    }
}
