package com.leds.leds.IMPL;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;

import com.leds.leds.dtos.CandidatosDto;
import com.leds.leds.dtos.ConcursosDto;
import com.leds.leds.models.Candidatos;
import com.leds.leds.models.Concursos;
import com.leds.leds.repositories.CandidatosRepository;
import com.leds.leds.repositories.ConcursosRepository;
import com.leds.leds.services.ConcursosService;

import jakarta.transaction.Transactional;

@Transactional
@Service
public class ConcursosIMPL implements ConcursosService {

    @Autowired
    private ConcursosRepository concursosRepository;

    @Autowired
    private CandidatosRepository candidatosRepository;

    @Override
    public ResponseEntity<List<ConcursosDto>> findAll() {
        try {
            List<Concursos> concursos = concursosRepository.findAll();
            return ResponseEntity.ok(concursos.stream().map(ConcursosDto::new).toList());
        } catch (Exception e) {
            System.err.println("Erro ao buscar todos os Concursos: " + e.getMessage());
            return ResponseEntity.internalServerError().build();
        }
    }

    @Override
    public ResponseEntity<List<CandidatosDto>> getBuscarCandidatosPorCodigoConcurso(String codigo) {
        try {
            Concursos concurso = concursosRepository.findByCodigoDoConcurso(codigo);

            if (concurso == null) {
                return ResponseEntity.notFound().build();
            }

            List<String> cargos = concurso.getListaDeVagas().stream()
                    .map(v -> v.getCargo().toLowerCase().trim())
                    .toList();

            List<Candidatos> candidatos = candidatosRepository.findByProfissoesIn(cargos);

            List<CandidatosDto> dtos = candidatos.stream()
                    .map(CandidatosDto::new)
                    .toList();

            return ResponseEntity.ok(dtos);
        } catch (Exception e) {
            System.err.println("Erro ao buscar candidatos por código do concurso [" + codigo + "]: " + e.getMessage());
            return ResponseEntity.internalServerError().build();
        }
    }

}
