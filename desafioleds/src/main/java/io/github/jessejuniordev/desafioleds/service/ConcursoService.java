package io.github.jessejuniordev.desafioleds.service;

import io.github.jessejuniordev.desafioleds.model.Candidato;
import io.github.jessejuniordev.desafioleds.model.Concurso;
import io.github.jessejuniordev.desafioleds.repository.CandidatoRepository;
import io.github.jessejuniordev.desafioleds.repository.ConcursoRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.*;
import java.util.stream.Collectors;

@Service
public class ConcursoService {

    @Autowired
    private ConcursoRepository concursoRepository;

    @Autowired
    private CandidatoRepository candidatoRepository;

    /**
     * Encontrar candidatos que a profissão consiste com a vaga do concurso
     * @param codigoConcurso código do concurso
     * @return uma lista com os usuários que podem participar do concurso
     */
    public Set<Candidato> findCandidatoByCodigoConcurso(String codigoConcurso) {
        List<Concurso> concursos = concursoRepository.findByCodigoConcurso(codigoConcurso);

        if (concursos.isEmpty()) {
            throw new RuntimeException("Concurso não encontrado!");
        }

        Set<Candidato> candidatos = new HashSet<>();

        // para cada concurso encontrado
        for (Concurso concurso : concursos) {
            // obter as vagas
            Set<String> vagas = Arrays.stream(concurso.getVagas().replaceAll("[\\[\\]]", "").split(","))
                    .map(String::trim).collect(Collectors.toSet());


            // para cada candidato
            for (Candidato candidato : candidatoRepository.findAll()) {
                // capturar as profissoes do candidato
                Set<String> profissaoCandidato = Arrays.stream(candidato.getProfissoes().replaceAll("[\\[\\]]", "").split(","))
                        .map(String::trim).collect(Collectors.toSet());

                if (!Collections.disjoint(profissaoCandidato, vagas))
                    candidatos.add(candidato);
            }
        }
        return candidatos;
    }
}
