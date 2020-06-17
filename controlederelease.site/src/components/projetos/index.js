import React, { useEffect, useState } from 'react';
import HeaderSection from '../headerSection';
import Alert from '../Alert';
import api from '../../services/api';

import './style.css';

const Projetos = () => {

    const [id, setId] = useState(0);
    const [nome, setNome] = useState('');
    const [subpasta, setSubpasta] = useState('');
    const [operation, setOperation] = useState(1);
    const [projetos, setProjetos] = useState([]);
    const [versoes, setVersoes] = useState([]);
    const [alertVisible, setAlertVisible] = useState(false);
    const [alertContent, setAlertContent] = useState({});
    const [versoesSelected, setVersoesSelected] = useState([]);

    useEffect(() => {
        handleGetProjetosAsync();
        handleGetVersoesAsync();
    }, []);

    function toggle() {
        setAlertVisible(!alertVisible);
    }

    async function handleGetProjetosAsync() {
        try {
            const response = await api.get('projeto');

            if(response.data.sucess !== true) {
                return;
            };

            setProjetos(response.data.data);
        } catch (error) {
            console.log(error);
        }
    }

    async function handleGetVersoesAsync() {
        try {
            const response = await api.get('versaoProjeto');

            if(response.data.sucess !== true) {
                return;
            };

            setVersoes(response.data.data);
        } catch (error) {
            console.log(error);
        }
    }

    function handleSetVersoes(versao) {
        const index = versoesSelected
            ? versoesSelected.findIndex(v => v.id === versao.id)
            : -1;

        if(!versoesSelected) {
            setVersoesSelected([versao]);
            return;
        }

        if(index < 0) {
            setVersoesSelected([...versoesSelected, versao]);
        } else {
            const versoes = versoesSelected.filter(v => v.id !== versao.id);
            setVersoesSelected(versoes);
        }
    }

    function carregarDadosProjeto(projeto) {
        setId(projeto.id);
        setNome(projeto.nome);
        setSubpasta(projeto.subpasta);
        setVersoesSelected(projeto.versoes);
    }

    function setProjetoIni() {
        setId(0);
        setNome('');
        setSubpasta('');
        setVersoesSelected([]);
    }

    function cancelUpdate(event) {
        event.preventDefault();
        setProjetoIni();
    }

    function toogleProjectModal(event, operation, show = true) {

        event.preventDefault();

        if(nome === "" && operation === 0) return;

        setOperation(operation);
        toogleModal(show);
    }

    function toogleModal(show) {
        const $modal = document.getElementById('modal');
        $modal.style.display = show ? 'block' : 'none';
    }

    async function handleDeleteProjectAsync() {
        try {
            const response = await api.delete(`projeto/${id}`);
            
            handleGetProjetosAsync();
        } catch (error) {
            console.log(error);
        }

        toogleModal(false);
        setProjetoIni();
    }

    async function handleSaveProjectAsync() {
        try {
            const method = id ? 'put' : 'post';
            const params = id ? `/${id}` : '';
            let projeto = { 
                nome, 
                subpasta,
                versoes: versoesSelected
            }

            if(id) projeto = { ...projeto, id };

            const response = await api[method](`projeto${params}`, projeto);

            if(!response.data.sucess) {
                setAlertContent(response.data);
                toggle(true);
                toogleModal(false);
                return;
            }
            
            handleGetProjetosAsync();
        } catch (error) {
            console.log(error)
        }

        toogleModal(false);
        setProjetoIni();
    }

    function renderTableProjects() {
        return (
            <table className="table">
                <thead>
                    <tr>
                        <th scope="col">Id</th>
                        <th scope="col">Projeto</th>
                        <th scope="col">Subpasta</th>
                        <th scope="col">Versões</th>
                        <th scope="col">Ações</th>
                    </tr>
                </thead>
                <tbody>
                    {renderRowsTableProjects()}
                </tbody>
            </table>
        )
    }

    function renderVersoes(versoes) {
        if(versoes) {
            return versoes.reduce((current, versao) => {
                return current !== '' 
                    ? current = `${current}, ${versao.nome}`
                    : versao.nome;
            }, '');
        }
    }

    function renderRowsTableProjects() {
        return projetos.map(projeto => {
            return (
                <tr key={projeto.id}>
                    <th scope="row">{projeto.id}</th>
                    <td>{projeto.nome}</td>
                    <td>{projeto.subpasta}</td>
                    <td>{renderVersoes(projeto.versoes)}</td>
                    <th>
                        <button className="button-edit" onClick={() => carregarDadosProjeto(projeto)}>
                            <i className="material-icons">&#xE254;</i>
                        </button>
                        <button className="button-delete" onClick={e => {
                            carregarDadosProjeto(projeto);
                            toogleProjectModal(e, 1, true)
                        }} >
                            <i className="material-icons">&#xE872;</i>
                        </button>
                    </th>
                </tr>
            )
        })
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

            <HeaderSection title="Projetos" />
            
            <form className="form form-aligned">
                <div className="control-group">
                    <label htmlFor="name" >Projeto</label>
                    <input 
                        type="text" 
                        name="name" 
                        placeholder="Projeto..." 
                        className="input" 
                        value={nome} 
                        onChange={e => setNome(e.target.value) } 
                    />
                </div>
                <div className="control-group">
                    <label htmlFor="subpasta">Subpasta</label>
                    <input 
                        type="text" 
                        name="subpasta" 
                        placeholder="Subpasta..." 
                        className="input" 
                        value={subpasta} 
                        onChange={e => 
                        setSubpasta(e.target.value) } 
                    />
                </div>
                <div className="controls">
                    {versoes.map((versao, index) => (
                        <label htmlFor="aligned-cb" className="checkbox" key={versao.id}>
                            <input 
                                type="checkbox" 
                                checked={versoesSelected && versoesSelected.some(v => v.id === versao.id) ? true : false}
                                onChange={() => handleSetVersoes(versao)}
                            />
                                {versao.nome}
                        </label>
                    ))}
                </div>
                <div className="controls">
                    <button type="submit" className="button button-primary" onClick={e => {
                        setAlertVisible(false);
                        toogleProjectModal(e, 0, true)
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
                    return projetos.length === 0 ? <h1 className="message-result">Não há projetos cadastrados</h1> : renderTableProjects()
                })()}
            </div>
        </section>
    );
}

export default Projetos;