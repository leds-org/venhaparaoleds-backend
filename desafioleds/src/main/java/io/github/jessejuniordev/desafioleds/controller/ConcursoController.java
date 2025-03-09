package io.github.jessejuniordev.desafioleds.controller;

import io.github.jessejuniordev.desafioleds.model.Candidato;
import io.github.jessejuniordev.desafioleds.service.ConcursoService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.Set;

@RestController
@RequestMapping("/api/concursos")
public class ConcursoController {

    @Autowired
    private ConcursoService concursoService;

    @GetMapping("/candidatos/{codigoConcurso}")
    public ResponseEntity<Object> findByCodigoConcurso(@PathVariable String codigoConcurso) {
        if (codigoConcurso == null || codigoConcurso.length() != 11 || !codigoConcurso.matches("\\d+")) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                    .body("O código deve conter 11 digitos");
        }

        Set<Candidato> candidatos = concursoService.findCandidatoByCodigoConcurso(codigoConcurso);

        if (candidatos == null || candidatos.isEmpty()) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND)
                    .body("Nenhum candidato encontrado para o concurso de código: " + codigoConcurso);
        }

        return ResponseEntity.ok(candidatos);
    }

}
