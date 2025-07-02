package Pedro.Artur.BackendDesafioLeds.controller;

import Pedro.Artur.BackendDesafioLeds.model.Candidato;
import Pedro.Artur.BackendDesafioLeds.model.Concurso;
import Pedro.Artur.BackendDesafioLeds.service.CandidatoService;
import Pedro.Artur.BackendDesafioLeds.service.ConcursoService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/concurso")
public class ConcursoController {
    private final ConcursoService concursoService;
    private final CandidatoService candidatoService;

    public ConcursoController(ConcursoService concursoService, CandidatoService candidatoService){
        this.concursoService = concursoService;
        this.candidatoService = candidatoService;
    }

    @GetMapping
    public List<Concurso> getAll(){
        return concursoService.getAll();
    }

    @GetMapping(value = {"/{codigo}/candidatos"})
    public ResponseEntity<List<Candidato>> findCandidatos(@PathVariable Long codigo){
        List<Candidato> candidatos = candidatoService.findCandidatosCompativeis(codigo);
        return ResponseEntity.ok(candidatos);
    }
}
