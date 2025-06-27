package com.leds.leds.dtos;

import java.util.List;

import com.leds.leds.models.Concursos;
import com.leds.leds.models.Vagas;

import lombok.Data;

@Data
public class ConcursosDto {

    private Long id;
    private String orgao;
    private String edital;
    private String codigoDoConcurso;
    private List<VagasDto> listaDeVagas;

    public ConcursosDto(Concursos concurso) {
        this.id = concurso.getId();
        this.orgao = concurso.getOrgao();
        this.edital = concurso.getEdital();
        this.codigoDoConcurso = concurso.getCodigoDoConcurso();
        this.listaDeVagas = concurso.getListaDeVagas()
                .stream()
                .map(VagasDto::new)
                .toList();
    }
}
