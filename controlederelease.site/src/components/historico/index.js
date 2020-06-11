import React from 'react'
import HeaderSection from '../headerSection';

const Historico = () => {
    return (
        <section className="section">
            <HeaderSection title="Liberação Pasta Release  - 24/01/2020" />
            <table className="table">
                <thead>
                    <tr>
                        <th scope="col">Projeto</th>
                        <th scope="col">Versão release</th>
                        <th scope="col">Data versão release</th>
                        <th scope="col">Versão teste</th>
                        <th scope="col">Data versão teste</th>
                        <th scope="col">Status</th>
                    </tr>
                </thead>
                <tbody>
                    <tr className="status-mantido">
                        <td>Relatorios.exe</td>
                        <td>3.19.6</td>
                        <td>22/01/2020  14:49:13</td>
                        <td>3.19.6</td>
                        <td>22/01/2020  14:49:13</td>
                        <td>Mantido</td>
                    </tr>
                    <tr className="status-mantido">
                        <td>Atualizador.exe</td>
                        <td>3.19.6</td>
                        <td>22/01/2020  14:49:13</td>
                        <td>3.19.6</td>
                        <td>22/01/2020  14:49:13</td>
                        <td>Mantido</td>
                    </tr>
                    <tr className="status-atualizado">
                        <td>Pcdrug32.exe</td>
                        <td>3.19.6</td>
                        <td>22/01/2020  14:49:13</td>
                        <td>3.19.10</td>
                        <td>22/02/2020  14:49:13</td>
                        <td>Atualizadodo</td>
                    </tr>
                    <tr className="status-atualizado">
                        <td>Multpcd.exe</td>
                        <td>3.19.6</td>
                        <td>22/01/2020  14:49:13</td>
                        <td>3.19.10</td>
                        <td>22/02/2020  14:49:13</td>
                        <td>Atualizadodo</td>
                    </tr>
                </tbody>
            </table>
        </section>
    )
}

export default Historico;