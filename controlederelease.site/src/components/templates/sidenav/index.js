import React from 'react';
import { NavLink } from 'react-router-dom';

import './style.css';

const SideNav = () => {
    return (
        <nav className="side-nav">
            <ul>
                <li><NavLink to="/" exact>Home</NavLink></li>
                <li><NavLink to="/projetos">Projetos</NavLink></li>
                <li><NavLink to="/versoes">Versões</NavLink></li>
                <li><NavLink to="/historico">Histórico</NavLink></li>
            </ul>
        </nav>
    )
}

export default SideNav;