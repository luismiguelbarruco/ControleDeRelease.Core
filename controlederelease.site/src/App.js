import React from 'react';
import Header from './components/header';
import Sidenav from './components/sidenav';
import Router from './router';
import { BrowserRouter } from 'react-router-dom';

import './assets/css/reset.css';
import './assets/css/normalize.css';
import './assets/css/grid.css';
import './assets/css/global.css';

function App() {
	return (
        <BrowserRouter>
            <Header />
            <main className="main-container">
                <Sidenav />
                <Router />
            </main>
        </BrowserRouter>
  	);
}

export default App;
