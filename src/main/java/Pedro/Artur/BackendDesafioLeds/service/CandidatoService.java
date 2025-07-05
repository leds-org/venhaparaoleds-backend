package Pedro.Artur.BackendDesafioLeds.service;

import Pedro.Artur.BackendDesafioLeds.dtos.CandidatoResponseDTO;
import Pedro.Artur.BackendDesafioLeds.mapper.CandidatoMapper;
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

    public Candidato salvar(Candidato candidato){
        return candidatoRepository.save(candidato);
    }

    public Candidato BuscarPorCpf(String cpf){
        return candidatoRepository.findByCpf(cpf);
    }

    public List<CandidatoResponseDTO> BuscarCandidatosCompativeis(Long codigo){
        List<Concurso> concursos = concursoRepository.findByCodigo(codigo);

        Set<String> profissoes = concursos.stream()
                .flatMap(concurso -> concurso.getProfissoes().stream())
                .collect(Collectors.toSet());

        List<Candidato> candidatos = candidatoRepository.BuscarCandidatosCompativeis(profissoes);

        return candidatos.stream()
                .map(CandidatoMapper::toDTO)
                .collect(Collectors.toList());
    }
}
