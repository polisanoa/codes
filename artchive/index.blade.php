<!-- Nav bar displayed on every page -->
@extends('layouts.app')

@section('title', 'View Users')

@section('content')
    <div class="row justify-content-center">
    <div class="col-sm-11 col-md-8 col-md-offset-2">

    <h2 class="text-center">All Users</h2>
        @if(session()->get('success'))
            <div class="alert alert-success">
                {{ session()->get('success') }}
            </div><br />
        @endif
        <!-- TODO: Do we wanna implement a search or filter functionality on this page? -->
        {{-- <div class="form-group form-inline">
            <label>Search</label>
            <input style="margin-left: 5px" class="form-control col-sm-3"/>
        </div> --}}
        <table class="table table-striped">
            <thead>
                <tr>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Email</th>
                    <th>Phone Number</th>
                    <th></th>

                </tr>
            </thead>
          <tbody>
                @foreach($users as $user)
                <tr>
                    <td>{{$user->first_name}}</td>
                    <td>{{$user->last_name}}</td>
                    <td>{{$user->email}}</td>
                    <td>{{$user->phone_number}}</td>
                    <td><a href="{{ route('user.profile', $user->id) }}" class="btn btn-primary">Profile</a></td>
                    {{-- <td>
                        <form action="{{ route('users.destroy', $user->id)}}" method="POST">
                            @csrf
                            @method('DELETE')
                            <button class="btn btn-danger" type="submit">Delete</button>
                        </form>
                    </td> --}}
                </tr>
                @endforeach
          </tbody>

        </table>
        <div class="row d-flex justify-content-center" style="margin-top:20px;">
            {{ $users->links()}}
        </div>
    </div>
    </div>
@endsection
