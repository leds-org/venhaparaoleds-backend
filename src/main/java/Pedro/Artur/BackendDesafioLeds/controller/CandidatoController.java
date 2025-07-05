package Pedro.Artur.BackendDesafioLeds.controller;

import Pedro.Artur.BackendDesafioLeds.Utils.CpfUtils;
import Pedro.Artur.BackendDesafioLeds.dtos.ConcursoResponseDTO;
import Pedro.Artur.BackendDesafioLeds.model.Candidato;
import Pedro.Artur.BackendDesafioLeds.service.CandidatoService;
import Pedro.Artur.BackendDesafioLeds.service.ConcursoService;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/candidatos")
public class CandidatoController {
    private final CandidatoService candidatoService;
    private final ConcursoService concursoService;

    public CandidatoController(CandidatoService candidatoService, ConcursoService concursoService){
        this.candidatoService = candidatoService;
        this.concursoService = concursoService;
    }

    @GetMapping("/{cpf}/concursos")
    public List<ConcursoResponseDTO> BuscarConcursosCompativeisPorCpf(@PathVariable String cpf){
        return concursoService.buscarConcursosCompativeisPorCpf(CpfUtils.limpar(cpf));
    }


}
