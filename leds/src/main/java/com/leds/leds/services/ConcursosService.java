package com.leds.leds.services;

import java.util.List;

import org.springframework.http.ResponseEntity;

import com.leds.leds.dtos.CandidatosDto;
import com.leds.leds.dtos.ConcursosDto;

public interface ConcursosService {

    public ResponseEntity<List<ConcursosDto>> findAll();

    public ResponseEntity<List<CandidatosDto>> getBuscarCandidatosPorCodigoConcurso(String codigo);

}
