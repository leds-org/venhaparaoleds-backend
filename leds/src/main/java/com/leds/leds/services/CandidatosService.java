package com.leds.leds.services;

import java.util.List;

import org.springframework.http.ResponseEntity;

import com.leds.leds.dtos.CandidatosDto;
import com.leds.leds.dtos.ConcursosDto;

public interface CandidatosService {

    ResponseEntity<List<CandidatosDto>> findAll();

    ResponseEntity<List<ConcursosDto>> getBuscarConcursosPorCpf(String cpf);

}
