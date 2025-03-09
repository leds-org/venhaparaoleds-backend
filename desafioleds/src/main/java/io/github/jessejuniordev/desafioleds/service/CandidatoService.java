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
public class CandidatoService {

    @Autowired
    private CandidatoRepository candidatoRepository;

    @Autowired
    private ConcursoRepository concursoRepository;

    /**
     * Encontrar uma vaga que consiste com a profissão de um candidato
     * @param cpf cpf do candidato
     * @return lista de concursos que o candidato pode participar
     */
    public List<Concurso> findConcursoByCpf(String cpf) {
        Optional<Candidato> candidatoCpf = candidatoRepository.findByCpf(cpf);

        if (candidatoCpf.isEmpty()) {
            return Collections.emptyList();
        }

        Candidato candidato = candidatoCpf.get();

        // converter a lista de profissao do candidato em uma string de profissões sem duplicação
        Set<String> profissaoCandidato = Arrays.stream(candidato.getProfissoes().replaceAll("[\\[\\]]", "").split(","))
                .map(String::trim)
                .collect(Collectors.toSet());

        List<Concurso> concursos = concursoRepository.findAll();

        // filtra os concursos onde a vaga bate com a profissão do candidato
        List<Concurso> concursosFiltrados = concursos.stream().filter(concurso -> {
            Set<String> vagasConcurso = new HashSet<>(Arrays.stream(concurso.getVagas().replaceAll("[\\[\\]]", "").split(",")).map(String::trim).toList());
            return !Collections.disjoint(profissaoCandidato, vagasConcurso);
        }).toList();

        return concursosFiltrados;
    }

}
