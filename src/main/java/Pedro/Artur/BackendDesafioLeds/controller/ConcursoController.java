package Pedro.Artur.BackendDesafioLeds.controller;

import Pedro.Artur.BackendDesafioLeds.dtos.CandidatoResponseDTO;
import Pedro.Artur.BackendDesafioLeds.model.Concurso;
import Pedro.Artur.BackendDesafioLeds.service.CandidatoService;
import Pedro.Artur.BackendDesafioLeds.service.ConcursoService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/concursos")
public class ConcursoController {
    private final ConcursoService concursoService;
    private final CandidatoService candidatoService;

    public ConcursoController(ConcursoService concursoService, CandidatoService candidatoService){
        this.concursoService = concursoService;
        this.candidatoService = candidatoService;
    }

    @GetMapping
    public List<Concurso> listarTodos(){
        return concursoService.listarTodos();
    }

    @GetMapping(value = {"/{codigo}/candidatos"})
    public ResponseEntity<List<CandidatoResponseDTO>> findCandidatos(@PathVariable Long codigo){
        List<CandidatoResponseDTO> candidatos = candidatoService.BuscarCandidatosCompativeis(codigo);
        return ResponseEntity.ok(candidatos);
    }
}
