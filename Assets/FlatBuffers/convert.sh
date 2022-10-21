#!/bin/sh

SCRIPTDIR=`cd "$(dirname "$0")" && pwd`

cd $SCRIPTDIR

flatc --csharp --gen-mutable $SCRIPTDIR/UserData.fbs

echo 'finish'