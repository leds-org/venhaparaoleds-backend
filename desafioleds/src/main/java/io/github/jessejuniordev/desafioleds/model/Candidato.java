package io.github.jessejuniordev.desafioleds.model;

import jakarta.persistence.*;
import lombok.Data;

import java.time.LocalDate;
import java.util.Objects;
import java.util.UUID;

@Entity
@Table(name = "candidatos")
@Data
public class Candidato {

    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    @Column(nullable = false)
    private UUID id;

    @Column(nullable = false)
    private String nome;

    @Column(name = "data_nascimento", nullable = false)
    private LocalDate dataNascimento;

    @Column(nullable = false)
    private String cpf;

    @Column(columnDefinition = "TEXT", nullable = false)
    private String profissoes;

    // os candidatos com os mesmos dados sejam tratados como iguais (evitar a duplicação pois temos concursos com o mesmo codigo)
    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Candidato candidato = (Candidato) o;
        return Objects.equals(cpf, candidato.cpf);  // considerar o cpf como identificador unico
    }

    @Override
    public int hashCode() {
        return Objects.hash(cpf);  // gerar hash baseado no CPF
    }

}
