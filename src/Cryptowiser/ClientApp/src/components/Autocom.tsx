import React, { ChangeEvent, Component } from 'react';  
import TextField from '@material-ui/core/TextField';  
import Autocomplete from '@material-ui/lab/Autocomplete';  
import AppBar from '@material-ui/core/AppBar';  
import Toolbar from '@material-ui/core/Toolbar'; 
import '../../src/custom.css';  
import axios from 'axios';  
import { IUser } from '../datasources/IUser';
import { authHeader } from '../_helpers/auth-header';
import Chip from '@material-ui/core/Chip';

interface IProps {
}

interface IRate{
    currency: string;
    value: number ;
}
interface IAutocomState {
    Symbols: string[];
    Symbol: string;
    rates: IRate[];
    errorMessage: string;
}

export class Autocom extends Component<IProps, IAutocomState> { 
        constructor(props: IProps) {  
                super(props)  
                this.state = {  
                        Symbols: [],
                        Symbol: "",
                        rates: [],
                        errorMessage: ""
                }
                
                this.onCurrencyChange = this.onCurrencyChange.bind(this);
        }  
        componentDidMount() {  
            
            const headers = authHeader();
          
              axios.get('api/crypto/symbols', { headers })
              .then(response => {
                
                this.setState({  
                    Symbols: response.data  
                });  
              })
              .catch(error => {
                
                  console.error('There was an error!', error);
              });

        }  

        onCurrencyChange = (event: ChangeEvent<{}>, values: string | null) => {
            this.setState({
                Symbol: values ?? ""
            }, () => {
              // This will output an array of objects
              // given by Autocompelte options property.
              console.log(this.state.Symbol);
            });
          }
      
        render() {  
            
                return (  
                        <div>  
     
                                <Autocomplete className="pding"  
                                        id="combo-box-demo"  
                                        onChange={this.onCurrencyChange}
                                        //onChange={(event: ChangeEvent<{}>, newValue: string[] | null) => this.onCurrencyChange(event, newValue ?? [])}
                                        options={this.state.Symbols}  
                                        getOptionLabel={option => option}  
                                        style={{ width: 300 }}  
                                        renderInput={params => (  
                                                <TextField {...params} label="Search Crypto Currency" variant="outlined" fullWidth /> 
                                        )}  
                                />  
                        </div>  
                )  
        }
        
        async populateCryptoRates() {
        
            const headers = authHeader();
        
            axios.get('api/crypto/rates', { headers })
            .then(response => {
              
              this.setState({ rates: response.data});
            })
            .catch(error => {
              
                this.setState({ errorMessage: error.message });
                console.error('There was an error!', error);
            });
        
        
          }
}  
export default Autocom  



