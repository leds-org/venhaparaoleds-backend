package Pedro.Artur.BackendDesafioLeds.model;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.NoArgsConstructor;

import java.util.ArrayList;
import java.util.List;


@AllArgsConstructor
@NoArgsConstructor
@Entity
@Table(name="tb_concurso")
public class Concurso {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private long id;

    private String orgao;
    private String edital;
    private Long codigo;
    private List<String> profissoes = new ArrayList<>();



}
