from flask import Flask, request, jsonify
from Projeto import f_LerArquivos, f_ConcursoPorCpf, f_CandidatoPorConcurso

app = Flask(__name__)

@app.route('/concursos/candidato', methods=['GET'])
def concursos_por_cpf():
    cpf = request.args.get('cpf')
    candidatos, concursos = f_LerArquivos()
    resultados = f_ConcursoPorCpf(candidatos, concursos, cpf)
    return jsonify(resultados)

@app.route('/candidatos/concursos', methods=['GET'])
def candidatos_por_concurso():
    codigo = request.args.get('codigo')
    candidatos, concursos = f_LerArquivos()
    resultados = f_CandidatoPorConcurso(candidatos, concursos, codigo)
    return jsonify(resultados)

if __name__ == '__main__':
    app.run(debug=True)
