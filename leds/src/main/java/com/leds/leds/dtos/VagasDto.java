package com.leds.leds.dtos;

import com.leds.leds.models.Vagas;

import lombok.Data;

@Data
public class VagasDto {

    private Long id;
    private String cargo;

    public VagasDto(Vagas vaga) {
        this.id = vaga.getId();
        this.cargo = vaga.getCargo();
    }
}
