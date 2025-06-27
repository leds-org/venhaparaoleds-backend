package com.leds.leds.dtos;

import java.util.List;

import com.leds.leds.models.Candidatos;

import lombok.Data;

@Data
public class CandidatosDto {

    private Long id;
    private String nome;
    private String dataNascimento;
    private String cpf;
    private List<ProfissaoDto> profissoes;

    public CandidatosDto(Candidatos candidato) {
        this.id = candidato.getId();
        this.nome = candidato.getNome();
        this.dataNascimento = candidato.getDataNascimento();
        this.cpf = candidato.getCpf();
        this.profissoes = candidato.getProfissoes().stream().map(ProfissaoDto::new).toList();
    }
}
