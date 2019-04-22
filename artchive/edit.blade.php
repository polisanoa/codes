@extends('layouts.app')

@section('content')
<div class="container">
    <div class="row justify-content-center">
        <div class="col-md-8">
            <div class="card card-blue">
                <div class="card-header">
                    <h4 style="padding-top: 10px;">Update Profile</h4>
                </div>
                <div class="card-body">
                    <form method="POST" action="{{ route('user.update', $user) }}"  enctype="multipart/form-data">
                        @csrf
                        @method('PATCH')

                        <!-- Input: Profile Photo -->
                        <div class="form-group row">
                            <label for="profile_photo" class="col-md-4 col-form-label text-md-right">Profile Photo</label>
                            <div class="col-md-6">
                                <img class="img-fluid" src="{{$user->profile_photo_url}}">
                                <br/>
                                <div class="control-group input-group">
                                    <input type="file" name="profile_photo" class="form-control">
                                </div>
                            </div>
                        </div>

                        <!-- Input: First Name -->
                        <div class="form-group row">
                            <label for="first_name" class="col-md-4 col-form-label text-md-right">First Name</label>
                            <div class="col-md-6">
                                <input id="first_name" type="text" class="form-control{{ $errors->has('first_name') ? ' is-invalid' : '' }}"
                                    name="first_name" value="{{ $user->first_name }}" required autofocus>

                                @if ($errors->has('first_name'))
                                    <span class="invalid-feedback" role="alert">
                                        <strong>{{ $errors->first('first_name') }}</strong>
                                    </span>
                                @endif
                            </div>
                        </div>

                        <!-- Input: Last Name -->
                        <div class="form-group row">
                            <label for="last_name" class="col-md-4 col-form-label text-md-right">Last Name</label>
                            <div class="col-md-6">
                                <input id="last_name" type="text" class="form-control{{ $errors->has('last_name') ? ' is-invalid' : '' }}"
                                    name="last_name" value="{{ $user->last_name }}" required autofocus>

                                @if ($errors->has('last_name'))
                                    <span class="invalid-feedback" role="alert">
                                        <strong>{{ $errors->first('last_name') }}</strong>
                                    </span>
                                @endif
                            </div>
                        </div>

                        <!-- Input: Email Address -->
                        <div class="form-group row">
                            <label for="email" class="col-md-4 col-form-label text-md-right">Email Address</label>
                            <div class="col-md-6">
                                <input id="email" type="email" class="form-control{{ $errors->has('email') ? ' is-invalid' : '' }}"
                                    name="email" value="{{ $user->email }}" required>

                                @if ($errors->has('email'))
                                    <span class="invalid-feedback" role="alert">
                                        <strong>{{ $errors->first('email') }}</strong>
                                    </span>
                                @endif
                            </div>
                        </div>

                        <!-- Input: Phone Number -->
                        <div class="form-group row">
                            <label for="phone_number" class="col-md-4 col-form-label text-md-right">Phone Number</label>
                            <div class="col-md-6">
                                <input id="phone_number" type="tel" class="form-control{{ $errors->has('phone_number') ? ' is-invalid' : '' }}"
                                    name="phone_number" value="{{ $user->phone_number }}"
                                    pattern="[0-9]{3}-[0-9]{3}-[0-9]{4}" required>

                                @if ($errors->has('phone_number'))
                                    <span class="invalid-feedback" role="alert">
                                        <strong>{{ $errors->first('phone_number') }}</strong>
                                    </span>
                                @endif
                            </div>
                        </div>

                        
                        <!-- Input: LinkedIn -->
                        <div class="form-group row">
                            <label for="link_linkedin" class="col-md-4 col-form-label text-md-right">LinkedIn</label>
                            <div class="col-md-6">
                                <input id="link_linkedin" type="text" class="form-control{{ $errors->has('link_linkedin') ? ' is-invalid' : '' }}"
                                    name="link_linkedin" value="{{ $user->link_linkedin }}" required>

                                @if ($errors->has('link_linkedin'))
                                    <span class="invalid-feedback" role="alert">
                                        <strong>{{ $errors->first('link_linkedin') }}</strong>
                                    </span>
                                @endif
                            </div>
                        </div>

                        
                        <!-- Input: Instagram -->
                        <div class="form-group row">
                            <label for="link_instagram" class="col-md-4 col-form-label text-md-right">Instagram</label>
                            <div class="col-md-6">
                                <input id="link_instagram" type="text" class="form-control{{ $errors->has('link_instagram') ? ' is-invalid' : '' }}"
                                    name="link_instagram" value="{{ $user->link_instagram }}" required>

                                @if ($errors->has('link_instagram'))
                                    <span class="invalid-feedback" role="alert">
                                        <strong>{{ $errors->first('link_instagram') }}</strong>
                                    </span>
                                @endif
                            </div>
                        </div>

                        <!-- Input: Biography -->
                        <div class="form-group row">
                            <label for="biography" class="col-md-4 col-form-label text-md-right">Biography</label>
                            <div class="col-md-6">
                                <textarea id="biography" type="text" class="form-control{{ $errors->has('biography') ? ' is-invalid' : '' }}"
                                        name="biography" rows="4">{{ $user->biography }}</textarea>

                                @if ($errors->has('biography'))
                                    <span class="invalid-feedback" role="alert">
                                        <strong>{{ $errors->first('biography') }}</strong>
                                    </span>
                                @endif
                            </div>
                        </div>

                        <!-- Button: 'Confirm' -->
                        <div class="form-group row mb-0">
                            <div class="col-md-8 offset-md-4">
                                <button type="submit" class="btn btn-primary col-sm-4">Confirm</button>
                                <a href="{{ route('user.show', $user) }}" class="btn btn-secondary col-sm-4">Cancel</a>
                            </div>
                        </div>
         
                        
                
                    </form>
                </div>
            </div>
        </div>
    </div>
</div>
@endsection
