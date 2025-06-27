package Pedro.Artur.BackendDesafioLeds.controller;

import Pedro.Artur.BackendDesafioLeds.model.Candidato;
import Pedro.Artur.BackendDesafioLeds.service.CandidatoService;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/candidato")
public class CandidatoController {
    private final CandidatoService candidatoService;

    public CandidatoController(CandidatoService candidatoService){
        this.candidatoService = candidatoService;
    }

    @GetMapping
    public List<Candidato> getAll(){
        return candidatoService.getAll();
    }

    @PostMapping
    public Candidato add(@RequestBody Candidato candidato){
        return candidatoService.save(candidato);
    }


}
