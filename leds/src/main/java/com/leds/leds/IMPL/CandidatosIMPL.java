package com.leds.leds.IMPL;

import java.util.List;
import java.util.Optional;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;

import com.leds.leds.dtos.CandidatosDto;
import com.leds.leds.dtos.ConcursosDto;
import com.leds.leds.models.Candidatos;
import com.leds.leds.models.Concursos;
import com.leds.leds.repositories.CandidatosRepository;
import com.leds.leds.repositories.ConcursosRepository;
import com.leds.leds.services.CandidatosService;

import jakarta.transaction.Transactional;

@Transactional
@Service
public class CandidatosIMPL implements CandidatosService {

    @Autowired
    private CandidatosRepository candidatosRepository;
    @Autowired
    private ConcursosRepository concursosRepository;

    @Override
    public ResponseEntity<List<CandidatosDto>> findAll() {

        List<Candidatos> concursos = candidatosRepository.findAll();
        return ResponseEntity.ok(concursos.stream().map(CandidatosDto::new).toList());
    }

    @Override
    public ResponseEntity<List<ConcursosDto>> getBuscarConcursosPorCpf(String cpf) {
        Optional<Candidatos> candidatoOpt = candidatosRepository.findByCpf(cpf);

        if (candidatoOpt.isEmpty()) {
            return ResponseEntity.notFound().build();
        }

        Candidatos candidato = candidatoOpt.get();

        List<String> profissoesCandidato = candidato.getProfissoes().stream()
                .map(p -> p.getNome().toLowerCase().trim())
                .toList();

        List<Concursos> concursos = concursosRepository.buscarPorProfissoes(profissoesCandidato);

        List<ConcursosDto> concursosDto = concursos.stream()
                .map(ConcursosDto::new)
                .toList();

        return ResponseEntity.ok(concursosDto);
    }

}
