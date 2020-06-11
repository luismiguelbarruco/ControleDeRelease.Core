import React, { useState, useEffect } from 'react';
import PageLoader from '../PageLoader/PageLoader';
import Alert from '../Alert';
import { Link } from 'react-router-dom';
import api from '../../services/api';

import './style.css';

const Home = () => {

    const [alertVisible, setAlertVisible] = useState(false);
    const [alertContent, setAlertContent] = useState({});
    const [loading, setLoading] = useState(false);
    const [analiseReleases, setAnaliseReleases] = useState([]);
    const [versoes, setVersoes] = useState([]);

    useEffect(() => {
        handleGetVersoesAsync();
    }, []);

    function toggle() {
        setAlertVisible(!alertVisible);
    }

    async function handleGetAnaliseAsync() {
        try {
            
            const $selectElemt = document.getElementById('versao');
            const id = $selectElemt.options[$selectElemt.selectedIndex].value;

            setLoading(true);

            const response = await api.get(`liberacaoRelease/${id}`);

            if(!response.data.sucess) {
                setAlertContent(response.data);
                toggle(true);
                return;
            }
            

            setAnaliseReleases(response.data);
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
        const date = `${today.getDate()}/${(today.getMonth()+1)}/${today.getFullYear()}`;
        return date;
    }

    function renderVersoesOption() {
        return (
            <select className="selectVersoes" id="versao">
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
                                <Link to="./" className="button-analize" onClick={handleGetAnaliseAsync}>Analisar</Link>
                            </React.Fragment>
                        )
                    }
                })()}
            </header>

            {(() => {
                if(analiseReleases && analiseReleases.length > 0) {
                    renderVersoesTable()
                }
            })}

            <Alert content={alertContent} isOpen={alertVisible} toggle={toggle}/>
            <PageLoader loading={loading}/>
        </section>
    )
}

export default Home;