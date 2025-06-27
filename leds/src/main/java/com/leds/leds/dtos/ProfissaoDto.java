package com.leds.leds.dtos;

import com.leds.leds.models.Profissao;

import lombok.Data;

@Data
public class ProfissaoDto {

    private Long id;
    private String nome;

    public ProfissaoDto(Profissao profissao) {
        this.id = profissao.getId();
        this.nome = profissao.getNome();
    }

    
}
