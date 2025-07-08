package Pedro.Artur.BackendDesafioLeds.service;

import Pedro.Artur.BackendDesafioLeds.dtos.ConcursoResponseDTO;
import Pedro.Artur.BackendDesafioLeds.exception.InvalidParameterException;
import Pedro.Artur.BackendDesafioLeds.exception.NotFoundCodigoException;
import Pedro.Artur.BackendDesafioLeds.mapper.ConcursoMapper;
import Pedro.Artur.BackendDesafioLeds.model.Concurso;
import Pedro.Artur.BackendDesafioLeds.repository.ConcursoRepository;
import Pedro.Artur.BackendDesafioLeds.utils.CodigoUtils;
import org.springframework.stereotype.Service;

import java.util.List;


@Service
public class ConcursoService {
    public final ConcursoRepository concursoRepository;

    public ConcursoService(ConcursoRepository concursoRepository){
        this.concursoRepository = concursoRepository;
    }

    public Concurso salvar(Concurso concurso){
        return concursoRepository.save(concurso);
    }

    public List<Concurso> buscarPorCodigo(Long codigo){
        List<Concurso> concursos = concursoRepository.findByCodigo(codigo);
        if(!CodigoUtils.validarCodigo(codigo)){
            throw new InvalidParameterException("Codigo invalido.");
        }

        if(concursos.isEmpty()){
            throw new NotFoundCodigoException();
        }
        return concursos;
    }

    public List<ConcursoResponseDTO> buscarPorProfissoes(List<String> profissoes){
        List<Concurso> concursos = concursoRepository.buscarConcursosCompativeisPorProfissoes(profissoes);
        return concursos.stream().map(ConcursoMapper::toDTO).toList();
    }
}

