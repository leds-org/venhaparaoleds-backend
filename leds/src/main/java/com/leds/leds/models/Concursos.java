package com.leds.leds.models;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

import jakarta.persistence.CascadeType;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.OneToMany;
import lombok.Data;

@Entity
@Data
public class Concursos implements Serializable {
    private static final long serialVersionUID = 1L;

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    private String orgao;
    private String edital;
    private String codigoDoConcurso;
    @OneToMany(mappedBy = "concurso", cascade = CascadeType.ALL, orphanRemoval = true)
    private List<Vagas> listaDeVagas = new ArrayList<>();

    
}
