package com.leds.leds.controllers;

import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import com.leds.leds.dtos.CandidatosDto;
import com.leds.leds.dtos.ConcursosDto;
import com.leds.leds.services.CandidatosService;
import com.leds.leds.services.ConcursosService;

import jakarta.persistence.Access;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;

@RestController
@RequestMapping("/concursos")
public class ConcusosController {

    @Autowired
    private ConcursosService concursosService;

    @GetMapping
    public ResponseEntity<List<ConcursosDto>> getAllConcursos() {
        return concursosService.findAll();
    }

        @GetMapping("/codigo")
    public ResponseEntity<List<CandidatosDto>> buscarCandidatosPorCodigoConcurso(@RequestParam String codigo) {
        return concursosService.getBuscarCandidatosPorCodigoConcurso(codigo);
    }

}
