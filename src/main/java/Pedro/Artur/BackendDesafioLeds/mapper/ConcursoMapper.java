package Pedro.Artur.BackendDesafioLeds.mapper;

import Pedro.Artur.BackendDesafioLeds.dtos.ConcursoResponseDTO;
import Pedro.Artur.BackendDesafioLeds.model.Concurso;

public class ConcursoMapper{

    public static ConcursoResponseDTO toDto(Concurso concurso){
        ConcursoResponseDTO concursoDto = new ConcursoResponseDTO();
        concursoDto.setOrgao(concurso.getOrgao());
        concursoDto.setCodigo(concurso.getCodigo());
        concursoDto.setEdital(concurso.getEdital());

        return concursoDto;
    }

}
