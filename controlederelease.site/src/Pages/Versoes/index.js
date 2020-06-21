import React, { useState, useEffect } from 'react';
import HeaderSection from '../../components/headerSection';
import Alert from '../../components/Alert';
import api from '../../services/api';

const Versoes = () => {

    const [alertVisible, setAlertVisible] = useState(false);
    const [alertContent, setAlertContent] = useState({});
    const [versoes, setVersoes] = useState([]);
    const [operation, setOperation] = useState(1);
    const [id, setId] = useState(0);
    const [nome, setNome] = useState('');
    const [diretorioRelease, setDiretorioRelease] = useState('');
    const [diretorioTeste, setDiretorioTeste] = useState('');

    useEffect(() => {
        handleGetVersoesAsync();
    }, []);

    function toggle() {
        setAlertVisible(!alertVisible);
    }

    function carregarDadosVersao(versao) {
        setId(versao.id);
        setNome(versao.nome);
        setDiretorioRelease(versao.diretorioRelease);
        setDiretorioTeste(versao.diretorioTeste);
    }

    function setVersaoInit() {
        setId(0);
        setNome('');
        setDiretorioRelease('');
        setDiretorioTeste('');
    }

    function cancelUpdate(event) {
        event.preventDefault();
        setVersaoInit();
    }

    function validarDados() {
        return (nome === "" || diretorioRelease === "" || diretorioTeste === "") ? false : true;
    }

    function toogleVersoestModal(event, operation, show = true) {

        event.preventDefault();

        if(!validarDados() && operation === 0) return;

        setOperation(operation);
        toogleModal(show);
    }

    function toogleModal(show) {
        const $modal = document.getElementById('modal');
        $modal.style.display = show ? 'block' : 'none';
    }

    async function handleDeleteProjectAsync() {
        try {
            const response = await api.delete(`versaoProjeto/${id}`);
            
            handleGetVersoesAsync();
        } catch (error) {
            console.log(error);
        }

        toogleModal(false);
        setVersaoInit();
    }
    
    async function handleSaveProjectAsync() {
        try {
            const method = id ? 'put' : 'post';
            const params = id ? `/${id}` : '';
            let versao = { nome, diretorioRelease, diretorioTeste }

            if(id) versao = { ...versao, id };

            const response = await api[method](`versaoProjeto${params}`, versao);

            if(!response.data.sucess) {
                setAlertContent(response.data);
                toggle(true);
                toogleModal(false);
                return;
            }

            handleGetVersoesAsync();
        } catch (error) {
            console.log(error);
            const response = { 
                message: 'Não foi possivel atualizar o projeto',
                data: []
            }

            setAlertContent(response.data);
            toggle(true);
            toogleModal(false);
            return;
        }

        toogleModal(false);
        setVersaoInit();
    }

    async function handleGetVersoesAsync() {
        try {
            const response = await api.get('versaoProjeto');

            if(!response.data.sucess) {
                return;
            };

            setVersoes(response.data.data);
        } catch (error) {
            console.log(error);
        }
    }
    
    function renderTableVersoes() {
        return (
            <table className="table">
                <thead>
                    <tr>
                        <th scope="col">Id</th>
                        <th scope="col">Nome</th>
                        <th scope="col">Pasta release</th>
                        <th scope="col">Pasta teste</th>
                        <th scope="col">Ações</th>
                    </tr>
                </thead>
                <tbody>
                    {renderRowsVersoes()}
                </tbody>
            </table>
        )
    }

    function renderRowsVersoes() {
        return (
            versoes.map(versao => {
                return (
                    <tr key={versao.id}>
                        <th scope="row">{versao.id}</th>
                        <td>{versao.nome}</td>
                        <td>{versao.diretorioRelease}</td>
                        <td>{versao.diretorioTeste}</td>
                        <td>
                            <button className="button-edit" onClick={() => carregarDadosVersao(versao)}>
                                <i className="material-icons">&#xE254;</i>
                            </button>
                            <button className="button-delete" onClick={e => {
                                carregarDadosVersao(versao);
                                toogleVersoestModal(e, 1, true)
                            }} >
                                <i className="material-icons">&#xE872;</i>
                            </button>
                        </td>
                    </tr>
                )
            })
        )
    }

    return (
        <section className="section">
            <div id="modal" className="modal">
                <div className="modal-content">
                    <div className="modal-header">
                        <h2 className="modal-title">Control de release</h2>
                        <button type="button" className="close-button-modal" onClick={() => toogleModal(false)}>
                            <span>×</span>
                        </button>
                    </div>
                    <div className="modal-body">
                        <p>Deseja Confirmar a operação?</p>
                    </div>
                    <div className="modal-footer">
                        <button type="button" className="button" onClick={() => toogleModal(false)}>Fechar</button>
                        {(() => {
                            return operation === 0 ?
                            (<button type="button" className="button button-primary" onClick={handleSaveProjectAsync}>Confirmar</button>) :
                            (<button type="button" className="button button-primary" onClick={handleDeleteProjectAsync}>Confirmar</button>)
                        })()}
                    </div>
                </div>
            </div>

            <HeaderSection title="Versões" />

            <form className="form form-aligned">
                <div className="control-group">
                    <label htmlFor="nome">Nome</label>
                    <input 
                        type="text" 
                        name="nome" 
                        placeholder="Nome..." 
                        className="input" 
                        value={nome} 
                        onChange={e => setNome(e.target.value)}
                    />
                </div>    
                
                <div className="control-group">
                    <label htmlFor="pastaRelease">Pasta Release</label>
                    <input 
                        type="text" 
                        name="pastaRelease" 
                        placeholder="Pasta release..." 
                        className="input" 
                        value={diretorioRelease} 
                        onChange={e => setDiretorioRelease(e.target.value)}
                    />
                </div>

                <div className="control-group">
                    <label htmlFor="pastaTeste">Pasta Teste</label>
                    <input
                        type="text" 
                        name="pastaTeste" 
                        placeholder="Pasta teste..." 
                        className="input" 
                        value={diretorioTeste} 
                        onChange={e => setDiretorioTeste(e.target.value)}
                    />
                </div>
                
                <div className="controls">
                    <button type="submit" className="button button-primary" onClick={e => {
                        setAlertVisible(false);
                        toogleVersoestModal(e, 0, true);
                    }}>Salvar</button>
                    <button type="submit" className="button button-danger" onClick={e => {
                        setAlertVisible(false);
                        cancelUpdate(e);
                    }}>Cancelar</button>
                </div>
            </form>

            <Alert content={alertContent} isOpen={alertVisible} toggle={toggle}/>

            <div className="separator-top">
                {(() => {
                    return versoes.length === 0 ? <h1 className="message-result">Não há versões cadastradas</h1> : renderTableVersoes()
                })()}
            </div>
        </section>
    )
}

export default Versoes;