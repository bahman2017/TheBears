import React, { Component } from 'react'
import axios from 'axios';
import BearTable from './components/BearTable'
import AddBearForm from './components/AddBearForm'
import EditBearForm from './components/EditBearForm'



class App extends Component {
    constructor(props) {
        super(props)

        // Bind the this context to the handler function
        this.GetInfo = this.GetInfo.bind(this);
        this.DoEdit = this.DoEdit.bind(this);

        // Set some state
        this.state = {
            bears: []
        }
        this.IsEdit=0
        this.eBear = {}
    }
   

    
    componentDidMount() {
      this.GetInfo()
    }

    DoEdit(bear) {
        this.IsEdit = 1;
        this.eBear = bear;
        console.log(this.eBear)
        this.setState(this.state)
    }

    GetInfo() {
        axios.get('/bears')
            .then((response) => {
                this.IsEdit=0
                this.setState({ bears: response.data });
            })
            .catch(function (error) {
                console.log(error);
            });
    } 
   
    
    render() {
        return (
            <div className="container">
                <h1>The Bears</h1>
                <div className="flex-row">
                    <div className="flex-large">
                        {this.IsEdit == 0 ? (
                            <div>
                                <h2>Add Bear</h2>
                                <AddBearForm action={this.GetInfo} />
                            </div>) :
                            (
                            <div>
                                    <h2>Edit Bear</h2>
                                    <EditBearForm action={this.GetInfo} bear={this.eBear} />
                             </div>)
                       }
                    </div>
                    <div className="flex-large">
                        <h2>View bears</h2>
                        <BearTable bears={this.state.bears} action={this.GetInfo} doEdit={this.DoEdit} />
                    </div>
                </div>
            </div>
        );
    }
}


export default App