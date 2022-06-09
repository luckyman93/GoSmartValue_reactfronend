export interface IResponse<T>{
    version: string;
    statusCode:number;
    errorMessage: string;
    data:T;
}