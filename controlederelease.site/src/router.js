import React from 'react';
import { Switch, Route } from 'react-router-dom'
import Home from './Pages/home';
import Versoes from './Pages/Versoes';
import Projetos from './Pages/projetos';
import Historico from './Pages/historico';

const Router = () => (
    <Switch>
        <Route path='/' component={Home} exact/>
        <Route path='/projetos' component={Projetos} />
        <Route path='/versoes' component={Versoes} />
        <Route path='/historico' component={Historico} />
    </Switch>
);

export default Router;