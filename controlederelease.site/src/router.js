import React from 'react';
import { Switch, Route } from 'react-router-dom'
import Home from './components/home';
import Versoes from './components/Versoes';
import Projetos from './components/projetos';
import Historico from './components/historico';

const Router = () => (
    <Switch>
        <Route path='/' component={Home} exact/>
        <Route path='/projetos' component={Projetos} />
        <Route path='/versoes' component={Versoes} />
        <Route path='/historico' component={Historico} />
    </Switch>
);

export default Router;