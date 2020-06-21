import React from 'react';

import logo from '../../assets/images/scope.png';

const Logo = () => {
    return (
        <a className="header-logo" href="./index.html">
            <img src={logo} alt="logo"/>
            <span className="header-title"><strong>Controle de release</strong></span>
        </a>
    )
}

export default Logo;