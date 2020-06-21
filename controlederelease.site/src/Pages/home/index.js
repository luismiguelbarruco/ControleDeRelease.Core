import React, { useState, useEffect } from 'react';
import PageLoader from '../../components/PageLoader/PageLoader';
import Alert from '../../components/Alert';
import api from '../../services/api';

import './style.css';

const Home = () => {

    const [sucess, setSucess] = useState(false);
    const [versoes, setVersoes] = useState([]);
    const [loading, setLoading] = useState(false);
    const [alertContent, setAlertContent] = useState({});
    const [alertVisible, setAlertVisible] = useState(false);
    const [analiseReleases, setAnaliseReleases] = useState([]);
    
    useEffect(() => {
        handleGetVersoesAsync();
    }, []);

    function toggle() {
        setAlertVisible(!alertVisible);
    }

    function getLiberacaoReleases() {
        const $selectElemt = document.getElementById('versao');
        const id = $selectElemt.options[$selectElemt.selectedIndex].value;
        const versao = versoes.find(element => element.id === Number(id));

        return {
            versao,
            itens: analiseReleases
        };
    }

    async function handlePostAnaliseRelease() {
        try {
        
            setAlertVisible(false);

            const liberacaoReleases = getLiberacaoReleases();

            const response = await api.post('liberacaoRelease', liberacaoReleases);

            setSucess(response.data.sucess);

            if(!response.data.sucess) {
                setAlertContent(response.data);
                return;
            }

            setAlertVisible(true);

            setAlertContent({message : "Analise de liberação de release gravado com sucesso."});
            setAnaliseReleases([]);

        } catch (error) {
            console.log(error);
        }
    }

    async function handleGetAnaliseAsync() {
        try {
            
            setAlertVisible(false);
            setAnaliseReleases([]);
            
            const $selectElemt = document.getElementById('versao');
            const id = $selectElemt.options[$selectElemt.selectedIndex].value;

            setLoading(true);

            const response = await api.get(`liberacaoRelease/${id}`);

            setSucess(response.data.sucess);

            if(!response.data.sucess) {
                setAlertVisible(true);
                setAlertContent(response.data);
                return;
            }
            
            setAnaliseReleases(response.data.data);
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

    function getDate() {
        const today = new Date();
        const date = `${today.getDate()}/${(today.getMonth() + 1)}/${today.getFullYear()}`;
        return date;
    }

    function renderVersoesOption() {
        return (
            <select className="selectVersoes" id="versao" onChange={() => setAnaliseReleases([])}>
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
            analiseReleases.map(item => {
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

    return (
        <section className="section">
            <header className="header-section">
                <h1>Liberação Pasta Release  - {getDate()}</h1>
                {(() => {
                    if(versoes && versoes.length > 0) {
                        return (
                            <React.Fragment>
                                {renderVersoesOption()}
                                <button className="button" onClick={handleGetAnaliseAsync}>Analisar</button>
                                <button className="button button-primary" disabled={analiseReleases.length === 0} onClick={handlePostAnaliseRelease}>Salvar</button>
                            </React.Fragment>
                        )
                    }
                })()}
            </header>

            {(() => {
                if(analiseReleases && analiseReleases.length > 0) {
                    return renderVersoesTable()
                }
            })()}

            <Alert content={alertContent} isOpen={alertVisible} toggle={toggle} sucess={sucess}/>
            <PageLoader loading={loading}/>
        </section>
    )
}

export default Home;