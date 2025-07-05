package Pedro.Artur.BackendDesafioLeds.service;

import Pedro.Artur.BackendDesafioLeds.dtos.ConcursoResponseDTO;
import Pedro.Artur.BackendDesafioLeds.mapper.ConcursoMapper;
import Pedro.Artur.BackendDesafioLeds.model.Candidato;
import Pedro.Artur.BackendDesafioLeds.model.Concurso;
import Pedro.Artur.BackendDesafioLeds.repository.CandidatoRepository;
import Pedro.Artur.BackendDesafioLeds.repository.ConcursoRepository;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.stream.Collectors;

@Service
public class ConcursoService {
    public final ConcursoRepository concursoRepository;
    public final CandidatoRepository candidatoRepository;

    public ConcursoService(ConcursoRepository concursoRepository, CandidatoRepository candidatoRepository){
        this.concursoRepository = concursoRepository;
        this.candidatoRepository = candidatoRepository;
    }

    public Concurso salvar(Concurso concurso){
        return concursoRepository.save(concurso);
    }

    public List<Concurso> buscarPorCodigo(Long codigo){
        return concursoRepository.findByCodigo(codigo);
    }

    public List<ConcursoResponseDTO> buscarConcursosCompativeisPorCpf(String cpf){
        Candidato candidato = candidatoRepository.findByCpf(cpf);

        List<String> profissoes = candidato.getProfissoes();

        List<Concurso> concursos = concursoRepository.BuscarConcursosCompativeisPorCpf(profissoes);

        return concursos.stream()
                .map(ConcursoMapper::toDto)
                .collect(Collectors.toList());
    }

}
