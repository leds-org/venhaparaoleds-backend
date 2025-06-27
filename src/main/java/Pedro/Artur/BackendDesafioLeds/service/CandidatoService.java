package Pedro.Artur.BackendDesafioLeds.service;

import Pedro.Artur.BackendDesafioLeds.model.Candidato;
import Pedro.Artur.BackendDesafioLeds.repository.CandidatoRepository;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class CandidatoService {
    public final CandidatoRepository candidatoRepository;

    public CandidatoService(CandidatoRepository candidatoRepository) {
        this.candidatoRepository = candidatoRepository;
    }

    public List<Candidato> getAll(){
        return candidatoRepository.findAll();
    }

    public Candidato save(Candidato candidato){
        return candidatoRepository.save(candidato);
    }
}
