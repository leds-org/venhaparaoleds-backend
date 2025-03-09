def f_LerArquivos():
    candidatos = []
    concursos = []

    #lista de candidatos
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

    #lista dos concursos
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

def f_ConcursoPorCpf(candidatos,concursos,cpf):
    cpf = cpf.replace(".", "").replace("-", "")
    candidato = None

    for i in candidatos:
        if i["Cpf"] == cpf:
            candidato = i
            break
    
    if candidato is None:
        print("Candidato inexistente")

    for concurso in concursos:
        lista = concurso["lista_vagas"]

        #Tira as aspas e colchetes
        lista = lista.strip("[]")
        lista = lista.replace("'", "")

        #Transforma em lista
        lista = lista.split(",")

        for vaga in lista:
            if vaga.strip().lower() in candidato["profissoes"].lower():
                print(f"Órgão: {concurso['orgao']}")
                print(f"Código: {concurso['codigo']}")
                print(f"Edital: {concurso['edital']}")
                print("-" * 30)
                print("\n")

def f_CandidatoPorConcurso(candidatos,concursos,codigoC):
    encontrou = int()
    for concurso in concursos:
        if concurso["codigo"] == codigoC:
            lista = concurso["lista_vagas"]
    
            lista = lista.strip("[]")
            lista = lista.replace("'", "")

            #Transforma em lista
            lista = lista.split(",")

            for vaga in lista:
                for candidato in candidatos:
                        if vaga.strip().lower() in candidato["profissoes"].lower():
                            encontrou += 1 
                            print(f"Concurso: {concurso['orgao']} - {concurso['edital']}")

                            print(f"Nome: {candidato['nome']}")
                            print(f"Data de Nascimento: {candidato['data_nasc']}")
                            print(f"CPF: {candidato['Cpf']}")
                            print("-" * 30)
                            print("\n")
    
    if encontrou == 0:
        print("Não há candidatos compatíveis com este concurso!")
        print("\n")
def main():
    candidatos, concursos = f_LerArquivos()

    CpfTeste ="311.667.973-47" 
    #cpf = input("Digite o CPF do candidato: ")

    CodigoTeste = "61828450843"
    #Codigo = input("Digite o código do concurso: ")


    #f_ConcursoPorCpf(candidatos,concursos,CpfTeste)

    f_CandidatoPorConcurso(candidatos,concursos,CodigoTeste)



if __name__ == "__main__":
    main()