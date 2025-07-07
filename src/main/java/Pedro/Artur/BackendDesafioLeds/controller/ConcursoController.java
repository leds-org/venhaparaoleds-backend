package Pedro.Artur.BackendDesafioLeds.controller;

import Pedro.Artur.BackendDesafioLeds.dtos.CandidatoResponseDTO;
import Pedro.Artur.BackendDesafioLeds.service.MatchingService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/concursos")
public class ConcursoController {
    private final MatchingService matchingService;

    public ConcursoController(MatchingService matchingService){
        this.matchingService = matchingService;
    }

    @GetMapping(value = {"/{codigo}/candidatos"})
    public ResponseEntity<List<CandidatoResponseDTO>> buscarCandidatos(@PathVariable Long codigo) {
        return ResponseEntity.ok(matchingService.buscarCandidatosCompativeisPorCodigoConcurso(codigo));
    }
}
