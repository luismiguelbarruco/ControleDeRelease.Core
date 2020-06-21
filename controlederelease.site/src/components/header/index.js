import React from 'react';
import Logo from '../logo';
import { Link } from 'react-router-dom';
import { faSignOutAlt } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

import './style.css';

const Header = () => {
    return (
        <header className="header">
            <Logo />
            <nav className="header-menu">
                <ul>
                    <li>
                        <Link to="/">
                            <strong className="logout">Logout</strong>
                            <FontAwesomeIcon icon={faSignOutAlt} />
                        </Link>
                    </li>
                </ul>
            </nav>
        </header>
    )
}

export default Header;