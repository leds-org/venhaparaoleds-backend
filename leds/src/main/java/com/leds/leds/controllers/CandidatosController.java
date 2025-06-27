package com.leds.leds.controllers;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import com.leds.leds.dtos.CandidatosDto;
import com.leds.leds.dtos.ConcursosDto;
import com.leds.leds.services.CandidatosService;

@RestController
@RequestMapping("/candidatos")
public class CandidatosController {

    @Autowired
    private CandidatosService candidatosService;

    @GetMapping
    public ResponseEntity<List<CandidatosDto>> getAllCandidatos() {
        return candidatosService.findAll();
    }

    @GetMapping("/cpf")
    public ResponseEntity<List<ConcursosDto>> buscarConcursosPorCpf(@RequestParam String cpf) {
        return candidatosService.getBuscarConcursosPorCpf(cpf);
    }

}
