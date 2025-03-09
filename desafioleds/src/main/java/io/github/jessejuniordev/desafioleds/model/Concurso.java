package io.github.jessejuniordev.desafioleds.model;

import jakarta.persistence.*;
import lombok.Data;

import java.util.UUID;

@Entity
@Table(name = "concursos")
@Data
public class Concurso {

    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    @Column(nullable = false)
    private UUID id;

    @Column(length = 100, nullable = false)
    private String orgao;

    @Column(length = 10, nullable = false)
    private String edital;

    @Column(name = "codigo_concurso", length = 20, nullable = false)
    private String codigoConcurso;

    @Column(columnDefinition = "TEXT", nullable = false)
    private String vagas; // armazena dados com modelo: ["carpinteiro", "marceneiro"]
}
