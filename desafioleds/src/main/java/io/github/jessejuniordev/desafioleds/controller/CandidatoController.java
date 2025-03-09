package io.github.jessejuniordev.desafioleds.controller;

import io.github.jessejuniordev.desafioleds.model.Concurso;
import io.github.jessejuniordev.desafioleds.service.CandidatoService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequestMapping("/api/candidatos")
public class CandidatoController {

    @Autowired
    private CandidatoService candidatoService;

    @GetMapping("/concursos/{cpf}")
    public ResponseEntity<Object> findCandidatoByCpf(@PathVariable String cpf) {
        if (cpf == null || cpf.length() != 11 || !cpf.matches("\\d+")) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                    .body("O CPF deve conter exatamente 11 números");
        }

        List<Concurso> concursos = candidatoService.findConcursoByCpf(cpf);

        if (concursos == null || concursos.isEmpty()) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND)
                    .body("Nenhum concurso encontrado para o CPF informado");
        }

        return ResponseEntity.ok(concursos);
    }

}
