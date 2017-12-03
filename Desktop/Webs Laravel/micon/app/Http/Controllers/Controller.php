<?php

namespace App\Http\Controllers;
use Illuminate\Routing\Controller as BaseController;
use Illuminate\Http\request;
use DB;
class Controller extends BaseController
{
    public function form(Request $request){
        if($request->isMethod("post")&& $request->has("name"))
        {
          $name=$request->input("name");
        }
        else
        {
          $name="";
        }
        DB::insert('INSERT into usuario
                   (usuario)
                    values (?)', [$name]);
      return view("login",["name"=>$name]);
      }

      public function ver (Request $request){
        $datos=DB::select("SELECT* from usuario where usuario!=''");
        return view('ver',compact('datos'));
        }


        public function proto(Request $request1){
            if($request1->isMethod("post")&& $request1->has("email")&& $request1->has("password"))
            {
              $usuar=$request1->input("email");
              $pass=$request1->input("password");
              if (validar($usuar,$pass)==true)
              {
                  return view("Proto");
              }

            }
            else
            {
              return view("Home");
            }
            return view("Proto");
          }

          public function validar($em,$ps){
              $valid=false;
            if (
              (DB::select("SELECT usuario from logueo where usuario!=$em)==$em") &&
              (DB::select("SELECT clave from logueo where usuario!=$ps)==$ps")
              )
            {
            $valid=true;
            }

            return $valid;
          }
  }
