def f_LerArquivos():
    candidatos = []
    concursos = []

    # Lista de candidatos
    with open("candidatos.txt", "r", encoding="utf-8") as can:
        for linha in can.readlines()[1:]:
            linha = linha.strip()
            dados = linha.split(" | ")
            nome = dados[0]
            data_nasc = dados[1]
            cpf = dados[2]
            profissoes = dados[3]

            candidatos.append({
                "nome": nome,
                "data_nasc": data_nasc,
                "Cpf": cpf.replace(".", "").replace("-", ""),
                "profissoes": profissoes
            })

    # Lista de concursos
    with open("concursos.txt", "r", encoding="utf-8") as con:
        for linha in con.readlines()[1:]:
            linha = linha.strip()
            dados = linha.split(" | ")
            orgao = dados[0]
            edital = dados[1]
            codigo = dados[2]
            lista_vagas = dados[3]

            concursos.append({
                "orgao": orgao,
                "edital": edital,
                "codigo": codigo,
                "lista_vagas": lista_vagas
            })
        
    return candidatos, concursos

def f_ConcursoPorCpf(candidatos, concursos, cpf):
    cpf = cpf.replace(".", "").replace("-", "")
    candidato = None

    # Procurando o candidato pelo CPF
    for i in candidatos:
        if i["Cpf"] == cpf:
            candidato = i
            break

    if candidato is None:
        return []  # Retorna uma lista vazia, caso o candidato não seja encontrado

    resultados = []

    # Procurando os concursos que o candidato pode participar
    for concurso in concursos:
        lista = concurso["lista_vagas"]
        lista = lista.strip("[]").replace("'", "").split(",")

        for vaga in lista:
            if vaga.strip().lower() in candidato["profissoes"].lower():
                resultados.append({
                    "Código do Concurso": concurso["codigo"],
                    "Edital": concurso["edital"],
                    "Órgão": concurso["orgao"]
                })

    return resultados



def f_CandidatoPorConcurso(candidatos, concursos, codigoC):
    resultados = []

    for concurso in concursos:
        if concurso["codigo"] == codigoC:
            lista = concurso["lista_vagas"]
            lista = lista.strip("[]").replace("'", "").split(",")

            for vaga in lista:
                for candidato in candidatos:
                    if vaga.strip().lower() in candidato["profissoes"].lower():
                        resultados.append({
                            "Concurso": f"{concurso['orgao']} - {concurso['edital']}",
                            "Nome": candidato["nome"],
                            "Data de Nascimento": candidato["data_nasc"],
                            "CPF": candidato["Cpf"]
                        })

    return resultados