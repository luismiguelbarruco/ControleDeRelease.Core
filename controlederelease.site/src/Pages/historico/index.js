import React, { useState, useEffect } from 'react';
import PageLoader from '../../components/PageLoader/PageLoader';
import Alert from '../../components/Alert';
import api from '../../services/api';

import './style.css';

const Historico = () => {

    const [sucess, setSucess] = useState(false);
    const [versoes, setVersoes] = useState([]);
    const [loading, setLoading] = useState(false);
    const [alertContent, setAlertContent] = useState({});
    const [alertVisible, setAlertVisible] = useState(false);
    const [historicoReleases, setHistoricoReleases] = useState([]);
    
    useEffect(() => {
        handleGetVersoesAsync();
    }, []);

    function toggle() {
        setAlertVisible(!alertVisible);
    }

    async function handleGetHistoricoAsync() {
        try {
            
            setHistoricoReleases([]);
            const $selectElemt = document.getElementById('versao');
            const id = $selectElemt.options[$selectElemt.selectedIndex].value;

            setLoading(true);

            const response = await api.get(`historico/${id}`);

            if(!response.data.sucess) {
                setAlertVisible(true);
                setAlertContent(response.data);
                return;
            }
            
            setHistoricoReleases(response.data.data);
        } catch (error) {
            console.log(error);
        }
        finally {
            setLoading(false);
        }
    }

    async function handleGetVersoesAsync() {
        try {
            const response = await api.get('versaoProjeto');

            setVersoes(response.data.data);
        } catch (error) {
            console.log(error);
        }
    }

    function renderVersoesOption() {
        return (
            <select className="selectVersoes" id="versao" onChange={() => setHistoricoReleases([])}>
                {versoes.map(versao => <option key={versao.id} value={versao.id} onClick={() =>console.log('')}>{versao.nome}</option>)}
            </select>
        )
    }

    function renderVersoesTable() {
        return (
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
                    {renderRowsAnalise()}
                </tbody>
            </table>
        )
    }

    function renderRowsAnalise() {
        return (
            historicoReleases.items.map(item => {
                return (
                    <tr className={item.status === 'Atualizado'? 'status-atualizado' : 'status-mantido'} key={item.id}>
                        <th scope="row">{item.projeto}</th>
                        <td>{item.versaoRelease}</td>
                        <td>{item.dataVersaoRelease}</td>
                        <td>{item.versaoTeste}</td>
                        <td>{item.dataVersaoTeste}</td>
                        <td>{item.status}</td>
                    </tr>
                )
            })
        )
    }

    function renderTitleHeader() {
        return (
            <>
                <h1>
                    Liberação Pasta Release
                    {historicoReleases.items && historicoReleases.items.length > 0 &&  ` - ${historicoReleases.data}`}
                </h1>
            </>
        )
    }

    return (
        <section className="section">
            <header className="header-section">
                {renderTitleHeader()}
                {(() => {
                    if(versoes && versoes.length > 0) {
                        return (
                            <React.Fragment>
                                {renderVersoesOption()}
                                <button className="button" onClick={handleGetHistoricoAsync}>Buscar</button>
                            </React.Fragment>
                        )
                    }
                })()}
            </header>

            {(() => {
                if(historicoReleases.items && historicoReleases.items.length > 0) {
                    return renderVersoesTable()
                }
            })()}

            <Alert content={alertContent} isOpen={alertVisible} toggle={toggle} sucess={sucess}/>
            <PageLoader loading={loading}/>
        </section>
    )
}

export default Historico;