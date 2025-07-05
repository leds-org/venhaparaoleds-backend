package Pedro.Artur.BackendDesafioLeds.dtos;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@NoArgsConstructor
@AllArgsConstructor
@Data
public class ConcursoResponseDTO {
    private String orgao;
    private Long codigo;
    private String edital;

}
