package Pedro.Artur.BackendDesafioLeds.service;

import Pedro.Artur.BackendDesafioLeds.dtos.CandidatoResponseDTO;
import Pedro.Artur.BackendDesafioLeds.dtos.ConcursoResponseDTO;
import Pedro.Artur.BackendDesafioLeds.exception.NotFoundCpfException;
import Pedro.Artur.BackendDesafioLeds.mapper.CandidatoMapper;
import Pedro.Artur.BackendDesafioLeds.model.Candidato;
import Pedro.Artur.BackendDesafioLeds.repository.CandidatoRepository;
import Pedro.Artur.BackendDesafioLeds.utils.CpfUtils;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Set;


@Service
public class CandidatoService {
    public final CandidatoRepository candidatoRepository;

    public CandidatoService(CandidatoRepository candidatoRepository) {
        this.candidatoRepository = candidatoRepository;
    }

    public Candidato salvar(Candidato candidato){
        return candidatoRepository.save(candidato);
    }

    public Candidato buscarPorCpf(String cpf){
        Candidato candidato = candidatoRepository.findByCpf(CpfUtils.limpar(cpf));
        if(candidato == null){
            throw new NotFoundCpfException();
        }
        return candidato;
    }

    public List<CandidatoResponseDTO> buscarPorProfissoes(Set<String> profissoes){
        List<Candidato> candidatos = candidatoRepository.buscarCandidatosCompativeis(profissoes);
        return candidatos.stream().map(CandidatoMapper::toDTO).toList();
    }

}
